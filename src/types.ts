export interface CategoryDTO {
    id: string;
    name: string;
}

export interface HabitResponseDTO {
    id: string;
    name: string;
    category: string;
    isCompleted: boolean;
    createdOn: string;
    modifiedOn: string;
}

export interface AddHabitRequestDTO {
    categoryId: string;
    name: string;
}