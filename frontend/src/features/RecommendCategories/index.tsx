import styles from "./index.module.scss";
import { Category } from "@entities/category";

interface RecommendCategoriesProps {
  categories: Category[];
}

export const RecommendCategories = ({
  categories,
}: RecommendCategoriesProps) => {
  return (
    <section className={styles.recommendation}>
      <h2 className={styles.recommendation__title}>
        Рекомендуемые
        <span className={styles["recommendation__title--accent"]}>
          <br />
          Категории
        </span>
      </h2>

      <ul className={styles.recommendation__list}>
        {categories.map((category) => (
          <li key={category.id} className={styles.recommendation__item}>
            <a className={styles.recommendation__link} href="#">
              <div className={styles["recommendation__image--wrapper"]}>
                <img
                  src={category.imageUrl}
                  alt={category.imageAlt}
                  className={styles.recommendation__image}
                />
                <h4 className={styles.recommendation__name}>{category.name}</h4>
              </div>
            </a>
          </li>
        ))}
      </ul>
    </section>
  );
};
