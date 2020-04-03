/*
  Properties of the main vehicle chassis.
  Added to the main vehicle parent, to allow each wheel to share state.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleProperties : MonoBehaviour
{
    // Controls virtual reality (VR) user interface
    public bool is_VR=false;
    
    // These are read by the wheels during setup
    public float max_angular_velocity=100.0f; // rads/sec wheel speed cap (hard limit seems to be like 35 rads/sec, equiv to 24mph)
    public float mass_tire=10.0f; // hung mass of tire and hub
    public float mass_engine=150.0f; // center mass of vehicle
    public float mass_vehicle;
    public float angular_drag_tire=0.001f; // per-tire angular drag
    
    // These are read by the wheels during driving
    public int drive_wheels=2;  // 2 == rear wheel drive.  4 == all wheel drive
    public float max_motor_torque=300.0f/2; // N-m of motor torque (per wheel)
    public float cur_motor_power=0.0f;
    public float cur_steer=0.0f;
    
    // Extracted physical motion of the vehicle
    private Rigidbody rb;
    public Vector3 last_velocity; // m/s velocity
    public Vector3 acceleration; // m/s^2 acceleration
    public float centripital; // sideways acceleration, in gravities
    public float drive; // m/s velocity relative to forward direction
    public float skid; // m/s velocity perpendicular to forward direction
    public float mph; // scalar velocity, in miles/hour
    
    // Follow camera
    public GameObject follow_camera;
    public Vector3 camera_position; // smoothed camera position
    public Vector3 next_camera() {
        float back=2.5f, up=1.0f;
        if (is_VR) { back=0.0f; up=0.0f; }
        return transform.position+(-back*transform.forward)+new Vector3(0.0f,up,0.0f);
    }
    
    // Debugging force visualization
    public Material force_material; // force shader (reads vertex colors)
    private const int nforces=1+4;
    private const int nforce_copies=10; // <- smoother bouncing
    private int force_index=0;
    private Color[]   force_color=new Color[nforces*nforce_copies]; // drawn color
    private Vector3[] force_start=new Vector3[nforces*nforce_copies]; // start location in world space (meters)
    private Vector3[] force_vec=new Vector3[nforces*nforce_copies]; // actual force vector (Newtons)
    public float force_scaling=1.0f/1000.0f; // force (N) to onscreen meters
    
    // For debugging terrain following
    public float vehicle_height;

    // Start is called before the first frame update
    void Start()
    {
        camera_position=next_camera();
    
        rb = GetComponent<Rigidbody>();
        rb.mass=mass_engine;
        mass_vehicle=mass_engine+4.0f*mass_tire;
        
        // GL rendering (not yet functional)
        Material mat = force_material;
        //Debug.Log("Set up material.");
    }
    
    // Need to register our callback with onPostRender
    void OnEnable() {
        Camera.onPostRender += draw_stored_lines;
    }
    void OnDisable() {
        Camera.onPostRender -= draw_stored_lines;
    }

    float atv_angle() {
        Vector3 atv_up = rb.transform.up;
        Vector3 world_up = Vector3.up;
        return Vector3.Angle(atv_up, world_up);
    }
    bool is_flipped() {
        float angle = atv_angle();
        if (angle >= 90.0f) {
             return true;
        }
        return false;
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
    Color force_average_color(Color[] force_arr,int idx) {
        Color ret=new Color(0.0f,0.0f,0.0f);
        for (int i=0;i<nforces*nforce_copies;i+=nforces)
            ret = ret + force_arr[i+idx];
        return ret*(1.0f/nforce_copies);
    }
    Vector3 force_average_vec3(Vector3[] force_arr,int idx) {
        Vector3 ret=new Vector3(0.0f,0.0f,0.0f);
        for (int i=0;i<nforces*nforce_copies;i+=nforces)
            ret = ret + force_arr[i+idx];
        return ret*(1.0f/nforce_copies);
    }
    
    // This gets called by the camera's OnPostRender callback
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
    
    // Draw a force vector onscreen, during later draw_stored_lines call.
    public void draw_force(Color c,Vector3 start,Vector3 force)
    { 
        force_index++; if (force_index>=nforces*nforce_copies) force_index=0;
        force_color[force_index]=c;
        force_start[force_index]=start;
        force_vec[force_index]=force;
    }
    
    // Blend slow with fast, by del
    public void complementary_filter(float del,ref float slow,float fast)
    {
        slow=slow*(1.0f-del)+fast*del;
    }
    public void complementary_filter(float del,ref Vector3 slow,Vector3 fast)
    {
        slow=slow*(1.0f-del)+fast*del;
    }
    
    // Return this 3D position floating safely over terrain
    public Vector3 flat_Y(Vector3 v) {
        v.y = 1.4f+Terrain.activeTerrain.SampleHeight(v);
        return v;
    }

    // Update is called once per physicsframe
    void FixedUpdate()
    {
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
        
        // Reset (after flip)
        if (Input.GetKey("r")) {
            transform.position=flat_Y(transform.position);
            transform.LookAt(flat_Y(transform.position+transform.forward*10.0f));
            rb.velocity=Vector3.ClampMagnitude(rb.velocity,5.0f); // limit linear velocity (don't zero it, for ice)
            rb.angularVelocity=new Vector3(0.0f,0.0f,0.0f);
        }
    }
}
