// import { DetailedHTMLProps, HTMLAttributes, useState, useRef } from "react";
// import { FC } from "react";
// import styles from "./index.module.scss";

// export interface InputProps
//   extends DetailedHTMLProps<
//     HTMLAttributes<HTMLInputElement>,
//     HTMLInputElement
//   > {
//   type: "text" | "password" | "email";
//   placeholder?: string;
//   label?: string;
// }

// export const Input: FC<InputProps> = ({
//   type,
//   placeholder = " ",
//   label,
// }: InputProps) => {
//   const [isFocused, setIsFocused] = useState(false);
//   const [hasValue, setHasValue] = useState(false);
//   const inputRef = useRef<HTMLInputElement>(null);

//   const handleFocus = () => setIsFocused(true);
//   const handleBlur = () => {
//     setIsFocused(false);
//     setHasValue(!!inputRef.current?.value);
//   };
//   const handleChange = () => setHasValue(!!inputRef.current?.value);

//   return (
//     <div className={styles.inputContainer}>
//       {label && (
//         <label
//           className={`${styles.label} ${
//             isFocused || hasValue ? styles.labelFloating : ""
//           }`}
//         >
//           {label}
//         </label>
//       )}
//       <input
//         ref={inputRef}
//         className={styles.input}
//         type={type}
//         placeholder={isFocused ? "" : placeholder}
//         onFocus={handleFocus}
//         onBlur={handleBlur}
//         onChange={handleChange}
//       />
//     </div>
//   );
// };
// import React, { forwardRef, useState, useEffect } from "react";
// import styles from "./index.module.scss";

// export interface InputProps
//   extends React.InputHTMLAttributes<HTMLInputElement> {
//   label?: string;
// }

// export const Input = forwardRef<HTMLInputElement, InputProps>(
//   (
//     {
//       type,
//       placeholder = " ",
//       label,
//       onFocus,
//       onBlur,
//       onChange,
//       value,
//       defaultValue,
//       ...rest
//     },
//     ref
//   ) => {
//     const [isFocused, setIsFocused] = useState(false);
//     const [hasValue, setHasValue] = useState(false);

//     useEffect(() => {
//       if (value !== undefined) {
//         setHasValue(Boolean(value));
//       } else if (defaultValue !== undefined) {
//         setHasValue(Boolean(defaultValue));
//       }
//     }, [value, defaultValue]);

//     const handleFocus = (e: React.FocusEvent<HTMLInputElement>) => {
//       setIsFocused(true);
//       onFocus?.(e);
//     };

//     const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
//       setIsFocused(false);
//       onBlur?.(e);
//     };

//     const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
//       setHasValue(Boolean(e.target.value));
//       onChange?.(e);
//     };

//     return (
//       <div className={styles.inputContainer}>
//         {label && (
//           <label
//             className={`${styles.label} ${
//               isFocused || hasValue ? styles.labelFloating : ""
//             }`}
//           >
//             {label}
//           </label>
//         )}
//         <input
//           ref={ref}
//           type={type}
//           placeholder={isFocused ? "" : placeholder}
//           className={styles.input}
//           onFocus={handleFocus}
//           onBlur={handleBlur}
//           onChange={handleChange}
//           value={value}
//           defaultValue={defaultValue}
//           {...rest}
//         />
//       </div>
//     );
//   }
// );

// Input.tsx
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
