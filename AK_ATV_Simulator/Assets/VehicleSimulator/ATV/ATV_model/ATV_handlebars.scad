/**
  Virtual model for the physical handlebars that the user is holding.
*/
dx=740/2; // left-right half size of handlebars
dz=-200; // front-back total size of handlebars
dr=sqrt(dx*dx+dz*dz); // pivot-to-tip length
ang=atan2(dz,dx); // pivot angle of handlebars (relative to a straight bar)
echo(dr);
echo(ang);

// Size of handlebars
radius=11;

// Mark out pivoting axle
rotate([-90,0,0])
	cylinder(d=20,h=20); 
	
/* Rotate out to end of handlebars */
module handletip(side=-1) {
	scale([side,1,1])
		rotate([0,-ang,0])
			translate([dr,0,0])
				children();
}

// Main bars of handlebars
for (side=[-1,+1]) 
	handletip(side)
		rotate([0,-90,0])
			cylinder(r=radius,h=dr);

// Box that connects to VIVE controller
handletip(-1)
	translate([-143,0,0])
		cube([40,40,30],center=true);



