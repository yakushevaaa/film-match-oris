import { DetailedHTMLProps, LabelHTMLAttributes, useState } from "react";
import styles from "./index.module.scss";
import cn from "classnames";
import { FC } from "react";

export interface CheckboxProps
  extends DetailedHTMLProps<
    LabelHTMLAttributes<HTMLLabelElement>,
    HTMLLabelElement
  > {
  label: string;
  value: string;
  isSelectAll?: boolean;
}

export const Checkbox: FC<CheckboxProps> = ({
  label,
  value,
  isSelectAll = false,
}) => {
  const [isChecked, setIsChecked] = useState(false);
  return (
    <div className={styles.checkbox_wrapper}>
      <label
        className={cn(styles.checkbox__label, {
          [styles.checkbox__label_selectAll]: isSelectAll,
        })}
      >
        <input
          className={cn(styles.checkbox__input, {
            [styles.checkbox__input_checked]: isChecked,
          })}
          type="checkbox"
          value={value}
          checked={isChecked}
          onChange={() => setIsChecked((prev) => !prev)}
        />
        <span className={cn({ [styles.checkbox__label_checked]: isChecked })}>
          {label}
        </span>
      </label>
    </div>
  );
};
