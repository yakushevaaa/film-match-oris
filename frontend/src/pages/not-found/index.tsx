import React from "react";
import { Link } from "react-router-dom";
import styles from "./index.module.scss";
import separatorIcon from "/icons/separator.svg";

export const NotFound = () => {
  return (
    <main className={styles.main}>
      <div className={styles.notFound}>
        <div className={styles.notFound__imgContainer}>
          <h1 className={styles.notFound__error}>404</h1>
        </div>
        <img className={styles.notFound__separator} src={separatorIcon} />
        <div className={styles.notFound__content}>
          <h1 className={styles.notFound__title}>
            This page could not be found
          </h1>
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
