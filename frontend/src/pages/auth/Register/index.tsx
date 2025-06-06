import { Logo } from "@shared/components/ui/Logo";
import { AuthForm } from "@features/AuthForm";
import arrowIcon from "/icons/arrow.svg";
import styles from "./index.module.scss";
import { Link } from "react-router-dom";

export const RegisterPage = () => {
  return (
    <div className={styles.container}>
      <div className={styles.welcome}>
        <Logo />
        <div className={styles.welcome__text}>
          <h2 className={styles.welcome__title}>Добро пожаловать!</h2>
          <p className={styles.welcome__subtitle}>
            Зарегистрируйтесь в системе
          </p>
        </div>
      </div>
      <div className={styles.form_container}>
        <div className={styles.form__head}>
          <h1 className={styles.form__title}>Регистрация</h1>
          <div className={styles.form__link_container}>
            <Link className={styles.form__link} to="login">
              <img className={styles.form__icon} src={arrowIcon} alt="" />
              Уже есть аккаунт?
            </Link>
          </div>
        </div>
        <AuthForm type="register" />
      </div>
    </div>
  );
};
