import logoIcon from "/icons/logo.svg";
import styles from "./index.module.scss";
export const Logo = () => {
  return (
    <div className={styles.logo_container}>
      <img className={styles.logo__img} src={logoIcon} alt="Logo" />
      <p className={styles.logo__name}>FilmMatch</p>
    </div>
  );
};
