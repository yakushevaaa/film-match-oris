export interface Category {
  id: number;
  name: string;
  imageUrl: string;
  imageAlt: string;
}

export interface CategoryProps {
  categoryData: Category;
}
