export interface Film {
    id: number;
    title: string;
    releaseDate: string;
    imageUrl: string | null;
    shortDescription: string;
    longDescription: string;
    categoryId: number;
    category: {
        id: number;
        name: string;
    };
} 