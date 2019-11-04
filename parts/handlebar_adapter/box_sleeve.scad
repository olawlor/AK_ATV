/**
 Fits inside 1.5" steel tube, and outside 1" steel tube.

*/
$fs=0.1; $fa=5;
inch=25.4; // file units: mm

wiggle=0.2;

OD=1.375*inch-2*wiggle;
ID=1.02*inch+2*wiggle;

round=4;

module inside_15x15() {
	offset(r=+round) offset(r=-round)
	difference() {
		square([OD,OD],center=true);
		// Notch for weld
		translate([OD*0.2,OD*0.5])
			square([OD*0.18,3],center=true);
	}
}

module outside_1x1() {
	round=2;
	offset(r=+round) offset(r=-round)
	square([ID,ID],center=true);
}

module part_2D() {
	difference() {
		inside_15x15();
		outside_1x1();
	}
}

linear_extrude(height=100.0, convexity=4) part_2D();

