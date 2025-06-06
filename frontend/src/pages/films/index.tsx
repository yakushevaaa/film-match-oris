import { Search } from "@shared/components/ui/Search";
import { Filters } from "@features/Filters";
import { CheckboxFiltersList } from "@features/CheckboxFiltersList";
import { GENRE_FILTERS } from "@features/CheckboxFiltersList/consts/filters";
import { FilmsList } from "@features/FilmsList";

import styles from "./index.module.scss";

export const FilmPage = () => {
  return (
    <div className={styles.film_page_container}>
      <div className={styles.top__container}>
        <Search width={500} placeholder="Найдите фильм" />
        <Filters />
      </div>
      <div className={styles.content}>
        <CheckboxFiltersList className={styles.filters} title="Жанры" items={GENRE_FILTERS} />
        <FilmsList />
      </div>
    </div>
  );
};
