import { useEffect, useState } from "react";
import styles from "./index.module.scss";
import { Category } from "@entities/category";
import axios from "axios";
import api from "@shared/api/axios";

export const CategoriesList = () => {
  const [categories, setCategories] = useState<Category[]>([]);

  useEffect(() => {
    axios;
    api.get("/Category/GetCategory").then((response) => {
      setCategories(response.data);
    });
  }, []);

  return (
    <section className={styles.categories}>
      <h2 className={styles.categories__title}>
        Все
        <span className={styles["categories__title--accent"]}>
          <br />
          Категории
        </span>
      </h2>
      <ul className={styles.categories__list}>
        {categories.map((category) => (
          <li key={category.id} className={styles.categories__item}>
            <a className={styles.categories__link} href="">
              <img
                className={styles.categories__img}
                src={category.imageUrl}
                alt={category.imageAlt}
              />
              <h5 className={styles.categories__name}>{category.name}</h5>
            </a>
          </li>
        ))}
      </ul>
    </section>
  );
};
