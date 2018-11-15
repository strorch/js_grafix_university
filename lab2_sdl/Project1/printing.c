#include "fdf.h"
static int	get_r(int color)
{
	return (color / 0x10000);
}

static int	get_g(int color)
{
	return ((color - (color / 0x10000) * 0x10000) / 0x100);
}

static int	get_b(int color)
{
	return (color % 0x100);
}

int			get_mid_color(int start, int end, double to_pass, double passed)
{
	int		r;
	int		g;
	int		b;
	double	rgb_steps[3];

	rgb_steps[0] = (get_r(end) - get_r(start)) / to_pass;
	rgb_steps[1] = (get_g(end) - get_g(start)) / to_pass;
	rgb_steps[2] = (get_b(end) - get_b(start)) / to_pass;
	r = get_r(start) + (rgb_steps[0] * passed);
	g = get_g(start) + (rgb_steps[1] * passed);
	b = get_b(start) + (rgb_steps[2] * passed);
	return (r * 0x10000 + g * 0x100 + b);
}

t_color		r_c(t_main *main)
{
	if (main->color % 7 == 0)
		return ((t_color) { 0xffFFff, 0xffffFF });
	else if (main->color % 7 == 1)
		return ((t_color) { 0xffFFff, 0x0000FF });
	else if (main->color % 7 == 2)
		return ((t_color) { 0xffFFff, 0x00FF00 });
	else if (main->color % 7 == 3)
		return ((t_color) { 0x00FFff, 0x008000 });
	else if (main->color % 7 == 4)
		return ((t_color) { 0x800000, 0x00FF00 });
	else if (main->color % 7 == 5)
		return ((t_color) { 0x7FFFD4, 0x893BFF });
	else if (main->color % 7 == 6)
		return ((t_color) { 0xADD8E6, 0xFFA500 });
	return ((t_color) { 0xffFFff, 0x0080FF });
}