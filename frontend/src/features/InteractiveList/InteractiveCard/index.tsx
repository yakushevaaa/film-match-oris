import { useEffect, useRef } from "react";
import { FilmProps } from "@entities/film";
import styles from "./index.module.scss";
import dislikeIcon from "/icons/dislike.svg";
import likeIcon from "/icons/like.svg";
import favoritesIcon from "/icons/favorites.svg";

interface AnimatedCardProps extends FilmProps {
  swipeDirection: "left" | "right" | null;
  onAnimationEnd: () => void;
}

export const InteractiveCard = ({
  filmData,
  onDislike,
  onLike,
  onFavorite,
  swipeDirection,
  onAnimationEnd,
}: AnimatedCardProps) => {
  const { id, title, imageUrl, longDescription, descriptionLong } = filmData;
  const description = longDescription || descriptionLong || '';
  const cardRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (swipeDirection && cardRef.current) {
      const handleAnimationEnd = () => {
        onAnimationEnd();
      };
      const node = cardRef.current;
      node.addEventListener("animationend", handleAnimationEnd);
      return () => node.removeEventListener("animationend", handleAnimationEnd);
    }
  }, [swipeDirection, onAnimationEnd]);

  return (
    <section id="interactive-card" className={styles["card-container"]}>
      <div
        ref={cardRef}
        className={`${styles.card} ${
          swipeDirection === "right"
            ? styles.swipeRight
            : swipeDirection === "left"
            ? styles.swipeLeft
            : ""
        }`}
      >
        <img className={styles.card__img} src={imageUrl} alt={title} />
        <h3 className={styles.card__title}>{title}</h3>
        <p className={styles.card__description}>{description}</p>
        <div className={styles["card__buttons-container"]}>
          <div className={styles["card__buttons-list"]}>
            <button className={styles.card__button} onClick={() => onDislike && onDislike(id)}>
              <img
                className={styles["card__button-icon"]}
                src={dislikeIcon}
                alt="Dislike Icon"
              />
            </button>
            <button className={styles.card__button} onClick={() => onFavorite && onFavorite(id)}>
              <img
                className={styles["card__button-icon"]}
                src={favoritesIcon}
                alt="Favorites Icon"
              />
            </button>
            <button className={styles.card__button} onClick={() => onLike && onLike(id)}>
              <img
                className={styles["card__button-icon"]}
                src={likeIcon}
                alt="Like Icon"
              />
            </button>
          </div>
        </div>
      </div>
    </section>
  );
};
