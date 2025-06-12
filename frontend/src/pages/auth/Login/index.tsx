import { Logo } from "@shared/components/ui/Logo";
import { AuthForm } from "@features/AuthForm";
import arrowIcon from "/icons/arrow.svg";
import styles from "./index.module.scss";

export const LoginPage = () => {
  return (
    <div className={styles.container}>
      <div className={styles.welcome}>
        <Logo />
        <div className={styles.welcome__text}>
          <h2 className={styles.welcome__title}>С возвращением!</h2>
          <p className={styles.welcome__subtitle}>Войдите в систему</p>
        </div>
      </div>
      <div className={styles.form_container}>
        <div className={styles.form__head}>
          <h1 className={styles.form__title}>Войти</h1>
          <div className={styles.form__link_container}>
            <a className={styles.form__link} href="">
              <img className={styles.form__icon} src={arrowIcon} alt="" />
              Создайте новую учетную запись
            </a>
          </div>
        </div>
        <AuthForm type="login" />
      </div>
    </div>
  );
};
