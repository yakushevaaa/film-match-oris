import { FilmProps } from "@entities/film";
import styles from "./index.module.scss";
import dislikeIcon from "/icons/dislike.svg";
import likeIcon from "/icons/like.svg";
import favoritesIcon from "/icons/favorites.svg";
export const InteractiveCard = ({
  filmData,
  onDislike,
  onLike,
  onFavorite,
}: FilmProps) => {
  const { id, title, imageUrl, longDescription, descriptionLong } = filmData;
  const description = longDescription || descriptionLong || '';
  return (
    <section id="interactive-card" className={styles["card-container"]}>
      <div className={styles.card}>
        <img className={styles.card__img} src={imageUrl} alt={title} />
        <h3 className={styles.card__title}>{title}</h3>
        <p className={styles.card__description}>{description}</p>
        <div className={styles["card__buttons-container"]}>
          <div className={styles["card__buttons-list"]}>
            <button className={styles.card__button}>
              <img
                className={styles["card__button-icon"]}
                src={dislikeIcon}
                alt="Dislike Icon"
                onClick={() => onDislike(id)}
              />
            </button>
            <button className={styles.card__button}>
              <img
                className={styles["card__button-icon"]}
                src={favoritesIcon}
                alt="Favorites Icon"
                onClick={() => onFavorite(id)}
              />
            </button>
            <button className={styles.card__button}>
              <img
                className={styles["card__button-icon"]}
                src={likeIcon}
                alt="Like Icon"
                onClick={() => onLike(id)}
              />
            </button>
          </div>
        </div>
      </div>
    </section>
  );
};
