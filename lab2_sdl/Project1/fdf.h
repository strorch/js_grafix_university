/* ************************************************************************** */
/*                                                                            */
/*                                                        :::      ::::::::   */
/*   wolf.h                                             :+:      :+:    :+:   */
/*                                                    +:+ +:+         +:+     */
/*   By: mstorcha <marvin@42.fr>                    +#+  +:+       +#+        */
/*                                                +#+#+#+#+#+   +#+           */
/*   Created: 2018/09/15 20:55:17 by mstorcha          #+#    #+#             */
/*   Updated: 2018/09/15 20:55:18 by mstorcha         ###   ########.fr       */
/*                                                                            */
/* ************************************************************************** */

#ifndef FDF_H
# define FDF_H

//# include "../libft/libft.h"
# include <math.h>
# include <stdio.h>
# include <stdlib.h>
# define HEIGHT 300
# define WIDTH 300

# define ANGLE_X	-20
# define ANGLE_Y	20
# define ANGLE_Z	-20

# include <stdio.h>


# include <SDL.h>


typedef struct	s_sdl
{
	SDL_Window		*window;
	SDL_Renderer	*rend;
	SDL_Surface		*sur;
	SDL_Texture		*text;
	SDL_Event		event;
}				t_sdl;


typedef struct	s_line
{
	char			*data;
	struct s_line	*next;
}				t_line;

typedef struct	s_coors
{
	int x;
	int y;
	int **z;
}				t_coors;

typedef struct	s_point
{
	float	x;
	float	y;
	float	z;
}				t_point;

typedef struct	s_zcoor
{
	int			min;
	int			max;
}				t_zcoor;

typedef struct	s_zpoint
{
	int			first;
	int			second;
}				t_zpoint;

typedef struct	s_main
{
	t_point		move;
	t_point		**arr;
	t_point		rot;
	t_point		koef;
	t_point		shifts;
	int			size;
	t_zcoor		min_max;
	int			color;
	t_sdl		sdl;
}				t_main;

typedef struct	s_color
{
	int			f;
	int			s;
}				t_color;

t_point			n_p(t_main p, t_point new);
void			mv_point(t_point ***arr1, t_point rot);
t_point			rotate_ort(t_point point, t_point rot);

#endif