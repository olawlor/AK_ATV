/*! \file VehicleProperties.cs
 * \brief The source for the class VehicleProperties
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*!Properties of the main vehicle chassis.
 * Added to the main vehicle parent, to allow each wheel to share state.
*/
public class VehicleProperties : MonoBehaviour
{
    /*!< Controls virtual reality (VR) user interface */
    public bool is_VR=false;

    /*!< rads/sec wheel speed cap (hard limit seems to be like 35 rads/sec, equiv to 24mph). Read by the wheels during setup. */
    public float max_angular_velocity=100.0f;
    /*!< hung mass of tire and hub. Read by the wheels during setup. */
    public float mass_tire=10.0f;
    /*!< center mass of vehicle. Read by the wheels during setup. */
    public float mass_engine=150.0f;
    /*!< Read by the wheels during setup. */
    public float mass_vehicle;
    /*!< per-tire angular drag. Read by the wheels during setup. */
    public float angular_drag_tire=0.001f;

    /*!< 2 == rear wheel drive.  4 == all wheel drive. Read by the wheels during driving */
    public int drive_wheels=2;
    /*!< N-m of motor torque (per wheel). Read by the wheels during driving */
    public float max_motor_torque=300.0f/2;
    /*!< Read by the wheels during driving */
    public float cur_motor_power=0.0f;
    /*!< Read by the wheels during driving */
    public float cur_steer=0.0f;

    /*!< Extracted physical motion of the vehicle */
    private Rigidbody rb;
    /*!< m/s velocity. Extracted physical motion of the vehicle */
    public Vector3 last_velocity;
    /*!< m/s^2 acceleration. Extracted physical motion of the vehicle */
    public Vector3 acceleration;
    /*!< sideways acceleration, in gravities. Extracted physical motion of the vehicle */
    public float centripital;
    /*!< m/s velocity relative to forward direction. Extracted physical motion of the vehicle */
    public float drive;
    /*!< m/s velocity perpendicular to forward direction. Extracted physical motion of the vehicle */
    public float skid;
    /*!< scalar velocity, in miles/hour. Extracted physical motion of the vehicle */
    public float mph; 

    public GameObject follow_camera;
    /*!< smoothed camera position. */
    public Vector3 camera_position; 
    public Vector3 next_camera() {
        float back=2.5f, up=1.0f;
        if (is_VR) { back=0.0f; up=0.0f; }
        return transform.position+(-back*transform.forward)+new Vector3(0.0f,up,0.0f);
    }

    /*! Driver properties */
    public GameObject driver;
    private Rigidbody driver_RB;
    public Transform head_position;
    /*!< DEBUG display vehicle center of mass. Part of driver properties */
    public Vector3 vehicle_COM;
    /*!< DEBUG display driver center of mass. Part of driver properties */
    public Vector3 driver_COM;
    /*!< DEBUG display vehicle local position relative to itself. Part of driver properties */
    public Vector3 vehicle_position;
    /*!< DEBUG display driver local position relative to the vehicle. Part of driver properties */
    private Vector3 driver_position; 
    private Vector3 driver_velocity;
    public Vector3 driver_last_position;
    public Vector3 driver_last_velocity;
    public Vector3 driver_acceleration;
    /*!< Part of driver properties. WORKAROUND: prevent liftoff during spawn */
    private bool allow_force = false; 
    IEnumerator WaitForTime(int t)
    {
        Debug.Log("Started waiting for " + t + " seconds");
        yield return new WaitForSeconds(t);
        Debug.Log("Finished waiting for " + t + "seconds!");
        allow_force = true;
    }

    /*!< force shader (reads vertex colors). Part of the debugging for force visualization */
    public Material force_material;
    /*!< The number of forces, the vehicle and the wheels */
    private const int nforces=1+4;
    /*!< smoother bouncing. Part of the debugging for force visualization */
    private const int nforce_copies=10; 
    private int force_index=0;
    /*!< drawn color. Part of the debugging for force visualization */
    private Color[]   force_color=new Color[nforces*nforce_copies];
    /*!< start location in world space (meters). Part of the debugging for force visualization */
    private Vector3[] force_start=new Vector3[nforces*nforce_copies];
    /*< actual force vector (Newtons). Part of the debugging for force visualization */
    private Vector3[] force_vec=new Vector3[nforces*nforce_copies];
    /*!< force (N) to onscreen meters. Part of the debugging for force visualization */
    public float force_scaling=1.0f/1000.0f; 
    
    /*! For debugging terrain following */
    public float vehicle_height;

    /*! Start is called before the first frame update. 
     This is will set mass of the vehicle.
     Seems as the previous team was not able to set up force materials.*/
    void Start()
    {
        camera_position=next_camera();
    
        rb = GetComponent<Rigidbody>();
        rb.mass=mass_engine;
        mass_vehicle=mass_engine+4.0f*mass_tire;
        driver_RB = driver.GetComponent<Rigidbody>();
        if (is_VR) StartCoroutine(WaitForTime(5));

        //! GL rendering (not yet functional)
        Material mat = force_material;
        //Debug.Log("Set up material.");
    }
    
    /*! Called when the object this script is attached to is enabled.
     Need to register our callback with onPostRender.
     onPostRender is an event function that is called after the camera renders the scene.
     Camera.onPostRender += draw_stored_lines calls the draw_stored_lines function.*/
    void OnEnable() {
        Camera.onPostRender += draw_stored_lines;
    }

    /*! Called when the object this script is attached to is disabled */
    void OnDisable() {
        Camera.onPostRender -= draw_stored_lines;
    }

    /*! Calculates the angle of the atv */
    float atv_angle() {
        Vector3 atv_up = rb.transform.up;
        Vector3 world_up = Vector3.up;
        return Vector3.Angle(atv_up, world_up);
    }

    /*! Determines if the atv has been flipped */
    bool is_flipped() {
        float angle = atv_angle();
        if (angle >= 90.0f) {
             return true;
        }
        return false;
    }

    /*! Returns the rigidbody of the object this script is attached to */
    public Rigidbody get_rb() {
        return rb;
    }

    // Average across the nforce_copies of idx in this array
    /*
    T force_average<T>(T[] force_arr,int idx) {
        T ret=new T(0.0f,0.0f,0.0f);
        for (int i=0;i<nforces*nforce_copies;i+=nforces)
            ret = ret + force_arr[i+idx];
        return ret*(1.0f/nforce_copies);
    }
    */

    /*! Determines the average color across the nforce_copies of idx in this array. */
    Color force_average_color(Color[] force_arr,int idx) {
        Color ret=new Color(0.0f,0.0f,0.0f);
        for (int i=0;i<nforces*nforce_copies;i+=nforces)
            ret = ret + force_arr[i+idx];
        return ret*(1.0f/nforce_copies);
    }

    /*! Determines the average vector across the nforce_copies of idx in this array. */
    Vector3 force_average_vec3(Vector3[] force_arr,int idx) {
        Vector3 ret=new Vector3(0.0f,0.0f,0.0f);
        for (int i=0;i<nforces*nforce_copies;i+=nforces)
            ret = ret + force_arr[i+idx];
        return ret*(1.0f/nforce_copies);
    }
    
    /*! Draws the lines that were stored in FixedUpdate.
     This gets called by the camera's OnPostRender callback. */
    void draw_stored_lines(Camera cam) {
        //Debug.Log("PostRender");
        GL.PushMatrix();
        force_material.SetPass(0);
        //GL.LoadProjectionMatrix(cam.projectionMatrix);
        //GL.LoadIdentity();
        //GL.MultMatrix(cam.transform.worldToLocalMatrix);
        GL.Begin(GL.LINES);
        
        /* stored forces */
        for (int i=0;i<nforces;i++) {
            GL.Color(force_average_color(force_color,i));
            Vector3 start=force_average_vec3(force_start,i);
            Vector3 vec=force_average_vec3(force_vec,i);
            GL.Vertex(start);
            GL.Vertex(start+vec*force_scaling);
        }
        
        
        /* //space-locked ground grid 
        GL.Color(new Color(1.0f,1.0f,1.0f,0.3f));
        float groundsz=50.0f;
        for (float dx=-groundsz;dx<=+groundsz;dx+=1.0f) {
            GL.Vertex(new Vector3(dx,0.0f,-groundsz));
            GL.Vertex(new Vector3(dx,0.0f,+groundsz));
        }
        for (float dz=-groundsz;dz<=+groundsz;dz+=1.0f) {
            GL.Vertex(new Vector3(-groundsz,0.0f,dz));
            GL.Vertex(new Vector3(+groundsz,0.0f,dz));
        }
        */
        
        /* //space-locked tics 
        for (float sx=-10.0f;sx<=+10.0f;sx+=1.0f) 
        for (float sy=0.0f;sy<=+0.4f;sy+=1.0f) 
        {
        if (sx<0.0) GL.Color(Color.red); else GL.Color(Color.green);
        for (float sz=-10.0f;sz<+10.0f;sz+=1.0f) {
            Vector3 start=new Vector3(sx,sy,sz); // transform.position;
            Vector3 force=new Vector3(0.0f,0.2f,0); // suspension.currentForce;
            GL.Vertex(start);
            GL.Vertex(start+force);
        }
        }
        */
        GL.End();
        GL.PopMatrix();
    }
    
    /*! Draw a force vector onscreen, during later draw_stored_lines call. */
    public void draw_force(Color c,Vector3 start,Vector3 force)
    { 
        force_index++; if (force_index>=nforces*nforce_copies) force_index=0;
        force_color[force_index]=c;
        force_start[force_index]=start;
        force_vec[force_index]=force;
    }
    
    /*! Blend slow with fast, by del */
    public void complementary_filter(float del,ref float slow,float fast)
    {
        slow=slow*(1.0f-del)+fast*del;
    }
    public void complementary_filter(float del,ref Vector3 slow,Vector3 fast)
    {
        slow=slow*(1.0f-del)+fast*del;
    }
    
    /*! Return this 3D position floating safely over terrain */
    public Vector3 flat_Y(Vector3 v) {
        v.y = Terrain.activeTerrain.SampleHeight(v) +
         Terrain.activeTerrain.transform.position.y + 2.0f;
        return v;
    }

    // Old: FixedUpdate is called once per physicsframe
    /*! FixedUpdate is called at a fixed interval of 0.02 seconds */
    void FixedUpdate()
    {
        //Debug.Log("FixedUpdate");
        if (follow_camera) {
            complementary_filter(is_VR?0.3f:0.02f,ref camera_position,next_camera());
            Vector3 next_position = camera_position;
            //if (is_VR) next_position.y=0.0f; // leave the floor as the floor
            follow_camera.transform.localPosition=next_position;
            
            Vector3 look_here=next_position+3.0f*transform.forward;
            if (is_VR) {
                look_here.y=camera_position.y; // never tilt head up and down
            } 
            follow_camera.transform.LookAt(look_here);
        }

        if (is_flipped() && !is_VR) {
            BroadcastMessage("StartRewind");
        }
        
        // Update vehicle center of mass physics
        float dt=Time.fixedDeltaTime;
        acceleration = (rb.velocity - last_velocity)/dt;
        centripital = Vector3.Dot(acceleration,transform.right)/9.81f;
        last_velocity = rb.velocity;

        // A = v^2/r
        // F = m*A
        draw_force(Color.green,transform.position,
            -acceleration*mass_vehicle);
        
        drive=Vector3.Dot(last_velocity,transform.forward);
        mph=drive*2.237f;
        skid=Vector3.Dot(last_velocity,transform.right);

        if (is_VR) {
            driver.transform.position = head_position.position;
            driver.transform.rotation = head_position.rotation;
            driver_velocity = (driver.transform.position - driver_last_position) / dt;
            driver_acceleration = (driver_RB.velocity - driver_last_velocity) / dt;
            if (allow_force) rb.AddForce(driver_RB.mass * driver_acceleration);

            //draw_force(Color.blue, driver.transform.position, -driver_acceleration*driver_RB.mass);

            driver_COM = driver_RB.centerOfMass;
            vehicle_COM = rb.centerOfMass;
            driver_position = driver.transform.localPosition;
            vehicle_position = rb.transform.localPosition;
            driver_last_position = driver.transform.position;
            driver_last_velocity = driver_velocity; // making our own velocity since the rigidbody isn't updating with respect to the physics engine
        }
    }
}
