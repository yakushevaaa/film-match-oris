import { DetailedHTMLProps, HTMLAttributes, useRef, useState } from "react";
import cn from "classnames";
import styles from "./index.module.scss";
import { Checkbox, CheckboxProps } from "@shared/components/ui/CheckBox";
import { FC } from "react";

interface CheckboxFiltersListProps {
  title: string;
  items: CheckboxProps[];
  onChange?: (value: string) => void;
  value?: string[];
  className?: string;
}

export const CheckboxFiltersList: FC<CheckboxFiltersListProps> = ({
  title,
  items = [],
  onChange,
  value = [],
  className,
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

  const handleCheckboxChange = (val: string) => {
    if (onChange) {
      onChange(val === value ? "" : val); // снимаем выбор при повторном клике
    }
  };

  return (
    <div className={cn(styles.checkbox_list, className)}>
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
              <Checkbox
                label={item.label}
                value={item.value}
                checked={value.includes(item.value as string)}
                onChange={() => handleCheckboxChange(item.value as string)}
              />
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};
