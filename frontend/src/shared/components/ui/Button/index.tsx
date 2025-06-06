import { DetailedHTMLProps, FC, HTMLAttributes, ReactNode } from "react";
import cn from "classnames";
import styles from "./index.module.scss";

interface ButtonProps
  extends DetailedHTMLProps<
    HTMLAttributes<HTMLButtonElement>,
    HTMLButtonElement
  > {
  children: ReactNode;
  appearance: "accent" | "primary";
  type?: "button" | "submit" | "reset";
}

export const Button: FC<ButtonProps> = ({
  children,
  appearance,
  className,
  type = "button",
  ...props
}) => {
  return (
    <button
      className={cn(styles.button, className, {
        [styles.button_accent]: appearance === "accent",
        [styles.button_primary]: appearance === "primary",
      })}
      type={type}
      {...props}
    >
      {children}
    </button>
  );
};
