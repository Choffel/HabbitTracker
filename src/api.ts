import axios from 'axios';
import type {CategoryDTO, HabitResponseDTO, AddHabitRequestDTO} from './types';

const API = axios.create({
    baseURL: 'https://localhost:44338/api/v1',
});

export const habitApi = {
    getAll: () => API.get<HabitResponseDTO[]>('/Habit'),
    create: (data: AddHabitRequestDTO) => API.post<HabitResponseDTO>('/Habit/Create', data),
    delete: (id: string) => API.delete(`/Habit/Delete/${id}`),
    complete: (id: string) => API.patch<HabitResponseDTO>(`/Habit/Complete/${id}`),
};

export const categoryApi = {
    getAll: () => API.get<CategoryDTO[]>('/Category/category/GetAll'),
    create: (name: string) => API.post<CategoryDTO>('/Category/category/Create', JSON.stringify(name), {
        headers: { 'Content-Type': 'application/json' }
    }),
};