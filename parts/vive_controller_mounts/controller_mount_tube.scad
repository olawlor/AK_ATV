/*
 Clamps a VIVE controller onto a round handlebar tube, held so 
  you can reach the trigger with your right thumb, like a thumb throttle.  
 */
$fs=0.1;
$fa=5;

inch=25.4;
wall=3.0;
support=25.6;
supportlen=50;
supportbar=[supportlen,support,support];
supportcenter=[-supportlen/2,0,0];

controller_z=110; // printed origin relative to controller origin (front of handle)

// Full 3D controller
module controller_3D() {
	// Hole for controller
	translate([0,-13,controller_z])
		rotate([90,0,0])
			hull()  //<- allows controller to drop in
			import("controller_body_simplified_v3.stl");
}

// Clearance for side buttons
module controller_sidebuttons() {
	translate([0,3,controller_z-96])
		rotate([5,0,0])
		hull() {
			for (topbot=[0,1]) translate([0,0,topbot*18])
				rotate([0,90,0]) cylinder(d=14,h=60,center=true);
		}
}


module controller_cross_2D(cutz, offset)
{
	translate([0,0,cutz])
		linear_extrude(height=0.1) 
			offset(r=+offset)
				projection(cut=true) 
					translate([0,0,-cutz]) 
						controller_3D();
}

module controller_hull_3D(fatten=0.0) {
	hull() {
		controller_cross_2D(0.0,fatten);
		controller_cross_2D(46.0,fatten);
	}
}

pipe_OD=0.845*inch;
pipe_center=[0,25,29];
pipe_len=40;
module support_pipe(fatten=0.0) {
	translate(pipe_center)
		rotate([0,90,0]) 
			cylinder(d=pipe_OD+2*fatten,h=pipe_len+(fatten==0?1:0),center=true);
}
module pipe_bolt_centers() {
	translate(pipe_center)
	for (hilo=[-1,+1]) for (LR=[-1,0,+1]) translate([LR*15,9,hilo*(pipe_OD+4)/2])
		rotate([90,0,LR*-5.0])
			children();
}


module controller_jacket_3D(outside=0.0) {
	difference() {
		union() {
			controller_hull_3D(wall);
			support_pipe(wall);
			hull() {
				translate([-10,0,0]) cube([20,15,20]);
				pipe_bolt_centers() 
				{
					cylinder(d=8,h=32);
				}
			}
		}
		
		union() {
			controller_hull_3D(0.1);
			// controller_hull_3D(0.1);
			controller_sidebuttons();
			support_pipe(0.0);
		
			// Clearance for the M3 bolt shafts:
			pipe_bolt_centers() cylinder(d=outside?3.1:2.3,h=25);
			
			// Clearance for the M3 round top sockets:
			pipe_bolt_centers() translate([0,0,0.1]) scale([1,1,-1]) cylinder(d1=6,d2=10,h=5);
		}
	}
}

// Main body
intersection() {
	controller_jacket_3D(0.0);
	translate([0,pipe_center[1]-1000,0]) cube([2000,2000,2000],center=true);
}

// Clamp-on chunk
translate([-pipe_center[2],0,pipe_len/2])
rotate([0,90,0])
intersection() {
	controller_jacket_3D(1.0);
	translate([0,pipe_center[1]+2+1000,0]) cube([2000,2000,2000],center=true);
}

