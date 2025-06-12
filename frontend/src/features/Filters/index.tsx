import { useState } from "react";
import cn from "classnames";
import styles from "./index.module.scss";
export const Filters = () => {
  const [activeFilter, setActiveFilter] = useState<
    "liked" | "new" | "no-filters"
  >("no-filters");
  return (
    <ul className={styles.filters}>
      <li className={styles.filters__item}>
        <label
          className={cn(styles.filters__label, {
            [styles.filters__label_active]: activeFilter === "liked",
          })}
        >
          <input
            className={styles.filters__input}
            type="radio"
            name="sort"
            onChange={() => setActiveFilter("liked")}
          />
          По цене
        </label>
      </li>
      <li className={styles.filters__item}>
        <label
          className={cn(styles.filters__label, {
            [styles.filters__label_active]: activeFilter === "new",
          })}
        >
          <input
            className={styles.filters__input}
            type="radio"
            name="sort"
            onChange={() => setActiveFilter("new")}
          />
          По новизне
        </label>
      </li>
    </ul>
  );
};
