import { InteractiveCard } from "@features/InteractiveCard";
import { useState } from "react";
import { Promo } from "@features/Promo";
import { RecommendCategories } from "@features/RecommendCategories";
import { CategoriesList } from "@features/CategoriesList";
import styles from "./index.module.scss";
import { useFilmActions } from "@/entities/film/useFilmActions";

export const MainPage = () => {
  const [filmData, setFilmData] = useState({
    id: 1,
    title: "Название фильма",
    releaseDate: "2024-01-01",
    imageUrl: "/images/film-pic.png",
    descriptionLong: "Длинное описание фильма...",
    descriptionShort: "Краткое описание фильма...",
    shortDescription: "Краткое описание фильма...",
    longDescription: "Длинное описание фильма...",
    categoryName: "Боевики",
    category: { id: 1, name: "Боевики" },
    description: "Питер Паркер — обычный подросток, живущий в Нью-Йорке. Он сталкивается с типичными проблемами, такими как учеба, отношения и жизнь с тетей Мэй и дядей Беном. После укуса паука он обнаруживает, что обладает сверхчеловеческими способностями: он может лазить по стенам, обладает повышенной силой и рефлексами, а также чувствует опасность благодаря паучьему чутью. После трагической смерти дяди Бена, Питер решает использовать свои способности для борьбы с преступностью, принимая на себя личину Человека-паука.Он сталкивается с различными врагами, включая Зелёного гоблина, который является одним из его самых известных противников.",
  });

  const categories = [
    {
      id: 1,
      name: "Боевики",
      imageUrl: "/images/category.webp",
      imageAlt: "Боевики",
    },
    {
      id: 2,
      name: "Комедии",
      imageUrl: "/images/category.webp",
      imageAlt: "Комедии",
    },
    {
      id: 3,
      name: "Драмы",
      imageUrl: "/images/category.webp",
      imageAlt: "Драмы",
    },
  ];

  const { handleLike, handleDislike, handleFavorite } = useFilmActions();

  return (
    <>
      <Promo />
      <InteractiveCard
        filmData={filmData}
        onDislike={handleDislike}
        onLike={handleLike}
        onFavorite={handleFavorite}
      />

      <RecommendCategories categories={categories} />
      <CategoriesList />
    </>
  );
};
