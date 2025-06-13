export interface Film {
  id: number;
  title: string;
  releaseDate: string;
  imageUrl: string;
  // imageAlt: string;
  descriptionLong: string;
  descriptionShort: string;
  shortDescription?: string;
  // categoryId: string;
  categoryName: string;
  category?: { id: number; name: string };
}

export interface FilmProps {
  filmData: Film;
  onLike?: (id: number) => void;
  onDislike?: (id: number) => void;
  onFavorite?: (id: number) => void;
}
