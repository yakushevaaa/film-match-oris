import { useEffect, useState } from "react";
import styles from "./index.module.scss";
import { Category } from "@entities/category";
import { axiosSettings } from "@shared/api/axiosSettings";
import { useNavigate } from "react-router-dom";

export const CategoriesList = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    axiosSettings
      .get("/Category/GetCategory")
      .then((response) => {
        setCategories(response.data);
      })
      .catch((error) => {
        console.error("Ошибка при получении категорий:", error);
      });
  }, []);

  const handleCategoryClick = (id: string | number) => {
    navigate(`/films?categoryId=${id.toString()}`);
  };

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
            <button
              className={styles.categories__link}
              onClick={() => handleCategoryClick(category.id)}
              style={{ background: "none", border: "none", padding: 0, cursor: "pointer" }}
            >
              <img
                className={styles.categories__img}
                src={category.imageUrl}
                alt={category.imageAlt}
              />
              <h5 className={styles.categories__name}>{category.name}</h5>
            </button>
          </li>
        ))}
      </ul>
    </section>
  );
};
