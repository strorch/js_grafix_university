#include "fdf.h"

void			mv_point(t_point ***arr1, t_point rot)
{
	t_point **arr = *arr1;
	t_point tmp;
	for (int i = 0; i < 6; i++)
		for (int j = 0; j < 10; j++)
		{
			tmp = arr[i][j];
			tmp.x = tmp.x + rot.x;
			tmp.y = tmp.y + rot.y;
			tmp.z = tmp.z + rot.z;
		}
}

t_point			rotate_ort(t_point point, t_point rot)
{
	t_point od;
	t_point dv;
	t_point tr;
	t_point rot_rad;

	rot_rad.x = rot.x * M_PI / 180.0;
	rot_rad.y = rot.y * M_PI / 180.0;
	rot_rad.z = rot.z * M_PI / 180.0;
	od.x = point.x;
	od.y = point.y * cos(rot_rad.x) + point.z * sin(rot_rad.x);
	od.z = point.z * cos(rot_rad.x) - point.y * sin(rot_rad.x);
	dv.x = od.x * cos(rot_rad.y) - od.z * sin(rot_rad.y);
	dv.y = od.y;
	dv.z = od.z * cos(rot_rad.y) + od.x * sin(rot_rad.y);
	tr.x = dv.x * cos(rot_rad.z) + dv.y * sin(rot_rad.z);
	tr.y = dv.y * cos(rot_rad.z) - dv.x * sin(rot_rad.z);
	tr.z = dv.z;
	return (tr);
}

/*
t_main			zoomed(t_main point)
{
	t_main	new;

	new.x = point.x;
	new.y = point.y;
	new.size = point.size;
	new.shifts.x = (new.x - 1) * new.size / 2;
	new.shifts.y = (new.y - 1) * new.size / 2;
	new.arr = create_point(new, point);
	new.rot = (t_point) { ANGLE_X, ANGLE_Y, ANGLE_Z };
	new.koef = point.koef;
	return (new);
}

t_point			n_p(t_main p, t_point new)
{
	return (mv_point(rotate_ort(new, p.rot),
		HEIGHT / 2 + p.koef.x, HEIGHT / 2 + p.koef.y));
}*/