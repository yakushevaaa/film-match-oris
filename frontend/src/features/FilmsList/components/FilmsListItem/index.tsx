import { FilmProps } from "@entities/film";
import likeIcon from "/icons/card-heart.svg";
import likeStrokeIcon from "/icons/card-heart-stroke.svg";
import { FC, useState } from "react";
import styles from "./index.module.scss";

export const FilmsListItem: FC<FilmProps> = ({ filmData, onLike }) => {
  const { id, title, releaseDate, imageUrl, categoryName } = filmData;
  const [liked, setLiked] = useState(false);

  const handleLikeClick = () => {
    if (onLike) {
      onLike(id);
      setLiked((prev) => !prev); // переключаем состояние
    }
  };

  return (
    <div className={styles.item}>
      <button className={styles.item__button} onClick={handleLikeClick}>
        <img
          className={styles.item__icon}
          src={liked ? likeIcon : likeStrokeIcon}
          alt="Лайк"
        />
      </button>

      <img className={styles.item__img} src={imageUrl} alt={title} />
      <div className={styles.item__data_container}>
        <h3 className={styles.item__title}>{title}</h3>
        <p className={styles.item__data}>
          <span>{releaseDate}</span> | <span>{categoryName}</span>
        </p>
      </div>
    </div>
  );
};
