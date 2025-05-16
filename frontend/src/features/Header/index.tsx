import { useLocation } from "react-router-dom";
import logoIcon from "/icons/logo.svg";
import styles from "./index.module.scss";
import cn from "classnames";

export const Header = () => {
  const location = useLocation();

  const renderHeader = () => {
    switch (location.pathname) {
      case "/":
        return (
          <header className={styles.header}>
            <img className={styles.header__logo} src={logoIcon} />
            <nav className={styles.nav}>
              <ul className={styles.nav__list}>
                <li className={styles.nav__item}>
                  <a
                    className={cn(styles.nav__link, styles.nav__link_accent)}
                    href=""
                  >
                    Рекомендуемые
                  </a>
                </li>
                <li className={styles.nav__item}>
                  <a className={styles.nav__link} href="">
                    Профиль
                  </a>
                </li>
              </ul>
            </nav>
          </header>
        );

      default:
        return (
          <header className={styles.header}>
            <img className={styles.header__logo} src={logoIcon} />
          </header>
        );
    }
  };

  return <>{renderHeader()}</>;
};
