import { DetailedHTMLProps, LabelHTMLAttributes, InputHTMLAttributes, useState } from "react";
import styles from "./index.module.scss";
import cn from "classnames";
import { FC } from "react";

export interface CheckboxProps extends Omit<DetailedHTMLProps<LabelHTMLAttributes<HTMLLabelElement>, HTMLLabelElement>, 'onChange'> {
  label: string;
  value?: string;
  isSelectAll?: boolean;
  checked?: boolean;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  inputProps?: InputHTMLAttributes<HTMLInputElement>;
  name?:string;
}

export const Checkbox: FC<CheckboxProps> = ({
  label,
  value,
  isSelectAll = false,
  checked,
  onChange,
  inputProps,
  ...rest
}) => {
  const [isChecked, setIsChecked] = useState(false);
  const isControlled = checked !== undefined;

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!isControlled) setIsChecked((prev) => !prev);
    onChange?.(e);
  };

  const inputChecked = isControlled ? checked : isChecked;

  return (
    <div className={styles.checkbox_wrapper}>
      <label
        className={cn(styles.checkbox__label, {
          [styles.checkbox__label_selectAll]: isSelectAll,
        })}
        {...rest}
      >
        <input
          className={cn(styles.checkbox__input, {
            [styles.checkbox__input_checked]: inputChecked,
          })}
          type="checkbox"
          value={value}
          checked={inputChecked}
          onChange={handleChange}
          {...inputProps}
        />
        <span className={cn({ [styles.checkbox__label_checked]: inputChecked })}>
          {label}
        </span>
      </label>
    </div>
  );
};
