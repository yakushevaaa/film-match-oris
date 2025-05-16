import styles from "./index.module.css";

export const Icon = ({ name }: { name: string }) => {
  return (
    <svg
      version="1.1"
      xmlns="http://www.w3.org/2000/svg"
      style={{
        width: `24px`,
        height: `24px`,
      }}
      className={styles.icon}
    >
      <use xlinkHref={`/sprite.svg#${name}`}></use>
    </svg>
  );
};
