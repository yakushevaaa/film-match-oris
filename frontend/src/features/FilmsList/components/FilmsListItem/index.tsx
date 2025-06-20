import { FilmProps } from "@entities/film";
import likeIcon from "/icons/card-heart.svg";
import likeStrokeIcon from "/icons/card-heart-stroke.svg";
import { FC, useState } from "react";
import styles from "./index.module.scss";

export const FilmsListItem: FC<FilmProps> = ({ filmData, onLike }) => {
  const { id, title, releaseDate, imageUrl, categoryName, shortDescription } =
    filmData;
  const [liked, setLiked] = useState(false);
  const [hovered, setHovered] = useState(false);

  const handleLikeClick = () => {
    if (onLike) {
      onLike(id);
      setLiked((prev) => !prev); 
    }
  };

  return (
    <div
      className={styles.item}
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
    >
      {/* <button className={styles.item__button} onClick={handleLikeClick}>
        <img
          className={styles.item__icon}
          src={liked ? likeIcon : likeStrokeIcon}
          alt="Лайк"
        />
      </button> */}

      <img className={styles.item__img} src={imageUrl} alt={title} />
      <div className={styles.item__data_container}>
        <h3 className={styles.item__title}>{title}</h3>
        <p className={styles.item__data}>
          <span>{releaseDate ? new Date(releaseDate).getFullYear() : ""}</span>
          {" | "}
          <span>{categoryName || filmData.category?.name || ""}</span>
        </p>
      </div>
      {hovered && (
        <div className={styles.item__overlay}>
          <span className={styles.item__description}>{shortDescription}</span>
        </div>
      )}
    </div>
  );
};
