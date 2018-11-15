/* ************************************************************************** */
/*                                                                            */
/*                                                        :::      ::::::::   */
/*   ft_makelist.c                                      :+:      :+:    :+:   */
/*                                                    +:+ +:+         +:+     */
/*   By: mstorcha <marvin@42.fr>                    +#+  +:+       +#+        */
/*                                                +#+#+#+#+#+   +#+           */
/*   Created: 2017/12/28 13:06:25 by mstorcha          #+#    #+#             */
/*   Updated: 2017/12/28 13:07:03 by mstorcha         ###   ########.fr       */
/*                                                                            */
/* ************************************************************************** */

#include "fdf.h"

t_line		*ft_create_elem(void *data)
{
	t_line *el;

	if (!(el = malloc(sizeof(t_line))))
		return (NULL);
	el->data = data;
	el->next = NULL;
	return (el);
}

int			ft_count(t_line *begin_list)
{
	int		i;

	i = 0;
	while (begin_list)
	{
		i++;
		begin_list = begin_list->next;
	}
	return (i);
}

void		*free_lst(t_line *list)
{
	while (list)
	{
		free(list->data);
		list = list->next;
	}
	free(list);
	return (NULL);
}

void		ft_list_push_back(t_line **begin_list, void *data)
{
	t_line *lst;
	t_line *new;

	lst = *begin_list;
	new = ft_create_elem(data);
	if (!lst)
	{
		*begin_list = new;
		return;
	}
	while (lst->next)
		lst = lst->next;
	lst->next = new;
}
