export interface Film {
  id: number;
  title: string;
  imageUrl: string;
  imageAlt: string;
  description: string;
}

export interface FilmProps {
  filmData: Film;
  onDislike: (id: number) => void;
  onLike: (id: number) => void;
  onFavorite: (id: number) => void;
}
