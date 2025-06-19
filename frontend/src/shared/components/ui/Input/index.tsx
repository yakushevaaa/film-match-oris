import React, { forwardRef, useState } from "react";
import styles from "./index.module.scss";
import cn from "classnames";

export interface InputProps
  extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
}

export const Input = forwardRef<HTMLInputElement, InputProps>(
  ({ label, type = "text", className, value, onFocus, onBlur, placeholder, ...rest }, ref) => {
    const [isFocused, setIsFocused] = useState(false);
    const hasValue = value !== undefined && value !== "";

    const handleFocus = (e: React.FocusEvent<HTMLInputElement>) => {
      setIsFocused(true);
      onFocus?.(e);
    };

    const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
      setIsFocused(false);
      onBlur?.(e);
    };

    return (
      <div className={styles.inputContainer}>
        <input
          ref={ref}
          type={type}
          placeholder={label ? " " : (placeholder || "")}
          className={cn(styles.input, className)}
          value={value}
          onFocus={handleFocus}
          onBlur={handleBlur}
          {...rest}
        />
        {label && (
          <label
            className={cn(
              styles.label,
              (isFocused || hasValue) && styles.labelFloating
            )}
          >
            {label}
          </label>
        )}
      </div>
    );
  }
);

Input.displayName = "Input";
