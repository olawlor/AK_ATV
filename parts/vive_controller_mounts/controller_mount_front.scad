/*
 Holds VIVE controller in front of handlebars.
*/

wall=3.0;
support=25.0;
supportlen=75;
supportbar=[supportlen,support,support];
supportcenter=[0,19,0];

// Full 3D controller
module controller_3D() {
	// Hole for controller
	#rotate([90,0,0])
		translate([0,125,13])
			import("controller_body_simplified_v3.stl");
}

// Approximates cross section of controller, to build walls.
module controller_cross_2D(dz) {
	hull() {
	translate([0,-5-0.20*dz]) scale([1.0,0.4]) circle(d=40+0.15*dz);
	translate([0,1+0.05*dz]) scale([1.38,1.0]) circle(d=25.5+0.15*dz);
	}
}

union() {
difference() {
	union() {
		// Main upright sidewall
		translate(supportcenter)
			translate([0,-wall,0])
				cube([supportbar[0],wall,supportbar[2]],center=true);
		
		// bars to connect to main body
		for (vertshift=[wall/2,19+wall+wall/2])
		translate(supportcenter)
			translate([0,-8-wall,-supportbar[2]/2+vertshift])
				hull() {
					translate([0,+10,0]) cube([supportbar[0],2*wall,wall],center=true);
					translate([0,-13,0]) cube([42,wall,wall],center=true);
				}
		
		
		//translate(supportcenter)
		//	cube([supportbar[0]-1,supportbar[1]+2*wall,supportbar[2]+wall],center=true);
		
	}
	
	// Holes for mounting bolts
	translate(supportcenter)
	for (side=[-1,+1])
	translate([side*(supportbar[0]/2-8),0,0])
		rotate([90,0,0])
			cylinder(d=4.5,h=50,center=true);
	
	controller_3D();
}

difference() {
	// Wraparound cylinder
	translate([0,0,-(supportbar[2])/2])
	hull()
	{
		linear_extrude(height=1,convexity=2) 
			offset(r=+wall) controller_cross_2D(-10.0);
		translate([0,0,32])
		linear_extrude(height=wall,convexity=2) 
			offset(r=+wall) controller_cross_2D(+10.0);
	}
	controller_3D();
}
}

