// import { DetailedHTMLProps, HTMLAttributes } from "react";
// import { FC } from "react";
// import styles from "./index.module.scss";

// interface InputProps
//   extends DetailedHTMLProps<
//     HTMLAttributes<HTMLInputElement>,
//     HTMLInputElement
//   > {
//   type: "text" | "password" | "email";
//   placeholder: string;
// }

// export const Input: FC<InputProps> = ({ type, placeholder }: InputProps) => {
//   return (
//     <input className={styles.input} type={type} placeholder={placeholder} />
//   );
// };
import { DetailedHTMLProps, HTMLAttributes, useState, useRef } from "react";
import { FC } from "react";
import styles from "./index.module.scss";

export interface InputProps
  extends DetailedHTMLProps<
    HTMLAttributes<HTMLInputElement>,
    HTMLInputElement
  > {
  id: string;
  type: "text" | "password" | "email";
  placeholder?: string;
  label?: string;
}

export const Input: FC<InputProps> = ({
  id,
  type,
  placeholder = " ",
  label,
}: InputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [hasValue, setHasValue] = useState(false);
  const inputRef = useRef<HTMLInputElement>(null);

  const handleFocus = () => setIsFocused(true);
  const handleBlur = () => {
    setIsFocused(false);
    setHasValue(!!inputRef.current?.value);
  };
  const handleChange = () => setHasValue(!!inputRef.current?.value);

  return (
    <div className={styles.inputContainer}>
      {label && (
        <label
          className={`${styles.label} ${
            isFocused || hasValue ? styles.labelFloating : ""
          }`}
        >
          {label}
        </label>
      )}
      <input
        key={id}
        ref={inputRef}
        className={styles.input}
        type={type}
        placeholder={isFocused ? "" : placeholder}
        onFocus={handleFocus}
        onBlur={handleBlur}
        onChange={handleChange}
      />
    </div>
  );
};
