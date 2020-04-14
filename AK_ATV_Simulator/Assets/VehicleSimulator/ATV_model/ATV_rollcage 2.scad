/**
 The roll cage is primarily designed to reduce simulator sickness,
 by providing clear high contrast co-moving geometry in peripheral vision.

 Looking cool helps too.
 
 Uses Unity Y-up coordinate system.  Vehicle drives in +Z direction.
 
 Press F6 to render, save as STL, load in MeshLab, save as OBJ, import into Unity.
*/

inch=25.4; // file units == millimeters
tube=1.5*inch; // 1.5 inch square tube (for low poly)


side=700.0;
high=1200.0;
front=1000.0;
slope=200.0;
back=-600.0;
pts=[
	[side,0,back,0],
	[side,0,back,-90],
	[side,high,back,-90],
	[side,high,back,0],
	[side,high,slope,0],
	[side,high,slope,+45],
	[side,high/2,front-100.0,45],
	[side,high/2,front-100.0,90],
	[side,0,front,90],
	[side,0,front,0],
];
npts=10;
cage_sidetilt=8;

module translate_point(p) {
	translate([pts[p][0],0,0])
	rotate([0,0,cage_sidetilt])
	translate([0,pts[p][1],pts[p][2]])
	rotate([0,0,-cage_sidetilt])
		children();
}

// Draw this vertex
module draw_point(p) {
	translate_point(p)
		rotate([pts[p][3],0,0])
			cube([tube,tube,1],center=true);
}

// Connect points p1 and p2 with a front-back bar
module draw_fbar(p1,p2) {
	hull() {
		draw_point(p1);
		draw_point(p2);
	}
}

// Draw a horizontal bar at this point
module draw_hbar(p) {
	hull() {
		for (leftright=[-1,+1]) scale([leftright,1,1])
		translate_point(p)
		cube([tube,tube,tube],center=true);
	}
}


for (leftright=[-1,+1]) scale([leftright,1,1])
{
	for (p=[0:npts-2]) {
		draw_fbar(p,p+1);
	}
	draw_fbar(npts-1,0);
}
draw_hbar(1);
draw_hbar(2);
translate([0,0,-tube/2]) draw_hbar(4);
draw_hbar(npts-1);
