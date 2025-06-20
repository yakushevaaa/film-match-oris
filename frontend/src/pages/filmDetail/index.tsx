import { useParams } from "react-router-dom";
import { InteractiveList } from "@/features/InteractiveList";
import styles from "./index.module.scss";

export const FilmDetail = () => {
  const { id } = useParams();

  return (
    <div className={styles.content}>
      <InteractiveList initialFilmId={id} />
    </div>
  );
};
