import { DetailedHTMLProps, HTMLAttributes, useRef, useState } from "react";
import cn from "classnames";
import styles from "./index.module.scss";
import { Checkbox, CheckboxProps } from "@shared/components/ui/CheckBox";
import { FC } from "react";

interface CheckboxFiltersListProps
  extends DetailedHTMLProps<HTMLAttributes<HTMLDivElement>, HTMLDivElement> {
  title: string;
  items: CheckboxProps[];
}

export const CheckboxFiltersList: FC<CheckboxFiltersListProps> = ({
  title,
  items = [],
}) => {
  const [isScrolled, setIsScrolled] = useState(false);
  const listRef = useRef<HTMLUListElement>(null);

  const handleScroll = () => {
    if (listRef.current) {
      setIsScrolled(listRef.current.scrollTop > 0);
    }
  };

  const selectAllItem = items.find((item) => item.isSelectAll);
  const otherItems = items.filter((item) => !item.isSelectAll);

  return (
    <div className={styles.checkbox_list}>
      <h4 className={styles.checkbox_list__title}>{title}</h4>
      <div className={styles.list}>
        {selectAllItem && (
          <div
            className={cn(styles.list__select_all, {
              [styles.list__select_all_scrolled]: isScrolled,
            })}
          >
            <Checkbox
              key={selectAllItem.value}
              label={selectAllItem.label}
              value={selectAllItem.value}
              isSelectAll={selectAllItem.isSelectAll}
            />
          </div>
        )}

        <ul
          className={styles.scrollable_list}
          ref={listRef}
          onScroll={handleScroll}
        >
          {otherItems.map((item) => (
            <li key={item.value} className={styles.scrollable_list__item}>
              <Checkbox label={item.label} value={item.value} />
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};
