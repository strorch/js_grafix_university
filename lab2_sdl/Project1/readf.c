#include "fdf.h"
t_point			mv_point(t_point point, int sh_x, unsigned int sh_y)
{
	t_point new;

	new.x = point.x + sh_x;
	new.y = point.y + sh_y;
	new.z = point.z;
	return (new);
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

static t_point	**create_point(t_main new, t_main point)
{
	int		i;
	int		j;
	t_point	**map;

	i = -1;
	map = (t_point **)malloc(sizeof(t_point *) * point.y);
	while (++i < point.y)
	{
		j = -1;
		map[i] = (t_point *)malloc(sizeof(t_point) * point.x);
		while (++j < point.x)
		{
			map[i][j].x = j * new.size - new.shifts.x;
			map[i][j].y = i * new.size - new.shifts.y;
			map[i][j].z = point.arr[i][j].z * point.size / 5.0;
		}
	}
	return (map);
}

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
}