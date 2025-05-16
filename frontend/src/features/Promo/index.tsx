import styles from "./index.module.scss";
import titleIcon from "/icons/promo-title.svg";
export const Promo = () => {
  return (
    <section className={styles.promo}>
      <div className={styles["promo__title-container"]}>
        <h1 className={styles.promo__title}>
          Открой мир
          <img className={styles.promo__icon} src={titleIcon} alt="" /> фильмов
        </h1>
        <p className={styles.promo__subtitle}>с каждым лайком</p>
      </div>
    </section>
  );
};
