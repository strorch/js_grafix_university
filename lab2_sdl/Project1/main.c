/* ************************************************************************** */
/*                                                                            */
/*                                                        :::      ::::::::   */
/*   main.c                                             :+:      :+:    :+:   */
/*                                                    +:+ +:+         +:+     */
/*   By: mstorcha <marvin@42.fr>                    +#+  +:+       +#+        */
/*                                                +#+#+#+#+#+   +#+           */
/*   Created: 2018/09/15 20:58:30 by mstorcha          #+#    #+#             */
/*   Updated: 2018/09/15 20:58:45 by mstorcha         ###   ########.fr       */
/*                                                                            */
/* ************************************************************************** */

#include "fdf.h"

#define K_K key.keysym.scancode
#define E_TYPE type

#define MAP_WIDTH 24
#define MAP_HEIGHT 24

void	draw_line(t_point first, t_point second, t_sdl *sdl)
{
	int		dx;
	int		dy;
	int		dz;
	float	k;
	float	t;

	k = 0.1 / sqrt((second.x - first.x) * (second.x - first.x)
		+ (second.y - first.y) * (second.y - first.y));
	t = -k;
	while ((t += k) <= 1)
	{
		dx = first.x + (second.x - first.x) * t;
		dy = first.y + (second.y - first.y) * t;
		printf("%d %d \n", dx, dy);
		if (dx * 300 + dy <= 0 || dx * 300 + dy >= 90000)
			continue;
		
		int *arr = sdl->sur->pixels;
		arr[dx * 300 + dy] = 0xff00ff;
	}
}

void	draw_function(t_sdl sdl, t_main mlx)
{
	t_point **new_p = malloc(6 * sizeof(t_point*));
	t_point tmp;
	for (int i = 0; i < 6; i++)
	{
		new_p[i] = malloc(10 * sizeof(t_point));
		for (int j = 0; j < 6; j++)
		{
			tmp.x = mlx.arr[i][j].x + mlx.move.x;
			tmp.y = mlx.arr[i][j].y + mlx.move.y;
			tmp.z = mlx.arr[i][j].z + mlx.move.z;
			new_p[i][j] = rotate_ort(tmp, mlx.rot);
		}
	}

	int i = -1;
	int j = -1;
	while (++i < 6)
	{
		j = -1;
		while (++j < 10)
		{
			if (j + 1 < 10)
				draw_line(mlx.arr[i][j], mlx.arr[i][j + 1], &sdl);
			if (i + 1 < 6)
				draw_line(mlx.arr[i][j], mlx.arr[i + 1][j], &sdl);
		}
	}
}
void	exit_message(const char *str)
{
	printf("%s", str);
	exit(0);
}


void	key_events(SDL_Event *event, t_main *mlx)
{
	(event->K_K == SDL_SCANCODE_ESCAPE) ? exit(0) : 0;
	//(event->K_K == SDL_SCANCODE_UP) ? (mlx->move.x += 5) : 0;
	//(event->K_K == SDL_SCANCODE_W) ? a : 0;

	/*
	(event->K_K == SDL_SCANCODE_UP) ? (mlx->scene->cam.d.x += 5) : 0;
	(event->K_K == SDL_SCANCODE_DOWN) ? (mlx->scene->cam.d.x -= 5) : 0;
	(event->K_K == SDL_SCANCODE_LEFT) ? (mlx->scene->cam.d.y += 5) : 0;
	(event->K_K == SDL_SCANCODE_RIGHT) ? (mlx->scene->cam.d.y -= 5) : 0;
	(event->K_K == SDL_SCANCODE_A) ? (mlx->scene->cam.p.x -= 0.5f) : 0;
	(event->K_K == SDL_SCANCODE_D) ? (mlx->scene->cam.p.x += 0.5f) : 0;
	(event->K_K == SDL_SCANCODE_W) ? (mlx->scene->cam.p.z += 0.5f) : 0;
	(event->K_K == SDL_SCANCODE_S) ? (mlx->scene->cam.p.z -= 0.5f) : 0;
	(event->K_K == SDL_SCANCODE_E) ? (mlx->scene->cam.p.y += 0.5f) : 0;
	(event->K_K == SDL_SCANCODE_Q) ? (mlx->scene->cam.p.y -= 0.5f) : 0;*/
}

void	sdl_events(SDL_Event *event, t_main *mlx)
{
	while (SDL_PollEvent(event))
	{
		if (event->E_TYPE == SDL_KEYDOWN)
			key_events(event, mlx);
		else if (event->E_TYPE == SDL_QUIT)
			exit(1);
	}
}

void	init_sdl(t_sdl *sdl)
{
	if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
		exit_message("Error in init sdl");
	if (!(sdl->window = SDL_CreateWindow("fdf",
		SDL_WINDOWPOS_UNDEFINED,
		SDL_WINDOWPOS_UNDEFINED, WIDTH, HEIGHT,
		SDL_WINDOW_SHOWN)))
		exit_message("Error creating window");
	if (!(sdl->rend = SDL_CreateRenderer(sdl->window, -1,
		SDL_RENDERER_ACCELERATED)))
		exit_message("Error in creating renderer");
	if (!(sdl->sur = SDL_CreateRGBSurface(0, WIDTH, HEIGHT, 32, 0, 0, 0, 0)))
		exit_message("Error in creating surface");
}

t_point **get_arr()
{
	t_point **arr = malloc(6 * sizeof(t_point*));

	for (int i = 0; i < 6; i++)
		arr[i] = malloc(10 * sizeof(t_point));
	for (int i = 0; i < 6; i++)
		for (int j = 0; j < 10; j++) {
			arr[i][j] = (t_point) { .x = i * 20 - 10 * 20 / 2, .y = -j * 20 - 6 * 20 / 2, .z = rand() % 20 };
			//arr[i][j] = rotate_ort(arr[i][j], (t_point) { .x = 20, .y = -20, .z = 20 });
		}
	return arr;
}

int main(int argc, char **argv)
{
	SDL_Event	event;
	t_sdl		sdl;
	SDL_Rect	dst_r;
	t_main		mlx;

	init_sdl(&sdl);

	mlx.arr = get_arr();
	mlx.move = (t_point) { .x = 0, .y = 0, .z = 0 };
	mlx.rot = (t_point) { .x = 20, .y = -20, .z = 20 };
	
	while (1)
	{
		sdl_events(&event, &(mlx));
		draw_function(sdl, mlx);

		sdl.text = SDL_CreateTextureFromSurface(sdl.rend, sdl.sur);
		SDL_RenderCopy(sdl.rend, sdl.text, NULL, NULL);
		SDL_RenderPresent(sdl.rend);
		SDL_DestroyTexture(sdl.text);
	}
	return (0);
}

