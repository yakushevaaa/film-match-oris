import { InteractiveCard } from "@features/InteractiveCard";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";
import { useState } from "react";
import { Film } from "@/entities/film";

export const FilmDetail = () => {
  const { id } = useParams();
  const [filmData, setFilmData] = useState<Film>({
    id: 1,
    title: "Название фильма",
    releaseDate: "2022-01-01", // можно указать корректную дату
    imageUrl: "/images/film-pic.png",
    descriptionShort: "Питер Паркер — обычный подросток, живущий в Нью-Йорке.",
    descriptionLong:
      "Питер Паркер — обычный подросток, живущий в Нью-Йорке. Он сталкивается с типичными проблемами, такими как учеба, отношения и жизнь с тетей Мэй и дядей Беном. После укуса паука он обнаруживает, что обладает сверхчеловеческими способностями: он может лазить по стенам, обладает повышенной силой и рефлексами, а также чувствует опасность благодаря паучьему чутью. После трагической смерти дяди Бена, Питер решает использовать свои способности для борьбы с преступностью, принимая на себя личину Человека-паука. Он сталкивается с различными врагами, включая Зелёного гоблина, который является одним из его самых известных противников.",
    categoryName: "Боевик",
  });

  return (
    <div>
      <InteractiveCard
        filmData={filmData}
        onLike={function (id: number): void {
          throw new Error("Function not implemented.");
        }}
        onDislike={function (id: number): void {
          throw new Error("Function not implemented.");
        }}
        onFavorite={function (id: number): void {
          throw new Error("Function not implemented.");
        }}
      />
    </div>
  );
};
