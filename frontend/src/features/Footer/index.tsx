import styles from "./index.module.scss";
import { useLocation } from "react-router-dom";
import logoIcon from "/icons/logo.svg";
import InstagramIcon from "/icons/social/instagram.svg";
import TelegramIcon from "/icons/social/telegram.svg";
import VkIcon from "/icons/social/vk.svg";

export const Footer = () => {
  return (
    <>
      <div className={styles.footer__container}>
        <section className={styles["pre-footer"]}>
          <h2 className={styles["pre-footer__title"]}>Чего же ты ждешь?</h2>
          <a className={styles["pre-footer__text"]} href="#interactive-card">
            НАЧНИ ВЫБИРАТЬ ФИЛЬМЫ ПРЯМО СЕЙЧАС
          </a>
        </section>
        <footer className={styles.footer}>
          <div className={styles.footer__column}>
            <img className={styles.footer__logo} src={logoIcon} alt="" />
            <p className={styles.footer__subtitle}>Video</p>
          </div>
          <div className={styles.footer__column}>
            <h4 className={styles.footer__title}>О проекте</h4>
            <ul className={styles.footer__list}>
              <li className={styles.footer__item}>
                <p className={styles.footer__text}>
                  Спорьте о фильмах меньше, а смотрите больше!
                </p>
                <p className={styles.footer__text}>
                  Наш алгоритм поможет найти идеальный вариант для компании
                  друзей, пары или семьи.
                </p>
              </li>
            </ul>
          </div>

          <div className={styles.footer__column}>
            <h4 className={styles.footer__title}>Контакты</h4>
            <ul className={styles.footer__list}>
              <li className={styles.footer__item}>
                <p className={styles.footer__text}>
                  Есть идеи или пожелания? Пишите: muzisnvlada@gmail.com
                </p>
              </li>
            </ul>
          </div>
          <div className={styles.contacts}>
            <ul className={styles.contacts__list}>
              <li className={styles.contacts__item}>
                <a className={styles.contacts__link} href="">
                  <img
                    className={styles.contacts__img}
                    src={InstagramIcon}
                    alt=""
                  />
                </a>
              </li>
              <li className={styles.contacts__item}>
                <a className={styles.contacts__link} href="">
                  <img
                    className={styles.contacts__img}
                    src={TelegramIcon}
                    alt=""
                  />
                </a>
              </li>
              <li className={styles.contacts__item}>
                <a className={styles.contacts__link} href="">
                  <img className={styles.contacts__img} src={VkIcon} alt="" />
                </a>
              </li>
            </ul>
          </div>
        </footer>
      </div>
    </>
  );
};
