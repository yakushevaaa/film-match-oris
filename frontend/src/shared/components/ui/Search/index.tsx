import searchIcon from "/icons/search-icon.svg";
import { DetailedHTMLProps, InputHTMLAttributes } from "react";
import { FC } from "react";
import styles from "./index.module.scss";

interface SearchProps
  extends DetailedHTMLProps<
    InputHTMLAttributes<HTMLInputElement>,
    HTMLInputElement
  > {
  width?: number | string;
}

export const Search: FC<SearchProps> = ({
  type,
  placeholder,
  width,
  ...props
}) => {
  return (
    <div className={styles.search_container} style={{ width }}>
      <img className={styles.search__icon} src={searchIcon} alt="" />
      <input
        className={styles.search}
        type={type}
        placeholder={placeholder}
        {...props}
      />
    </div>
  );
};
