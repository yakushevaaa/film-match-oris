import React from "react";
import { Link } from "react-router-dom";
import styles from "./index.module.scss"
import notFoundIcon from "/icons/404.svg";


export const NotFound = () => {
  return (
    <main className={styles.main}>
      <div className={styles.notFound}>
        <img className={styles.notFound__img} src={notFoundIcon} alt="" />
        <div className={styles.notFound__content}>
          <h1 className={styles.notFound__title}>This page could not be found</h1>
          <p className={styles.notFound__text}>
            You can either stay and chill here, or go back to the beginning.
          </p>
          <Link className={styles.notFound__link} to="/">
            Back to home
          </Link>
        </div>
      </div>
    </main>
  );
};
