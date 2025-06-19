import { FC } from "react";
import { NavLink } from "react-router-dom";
import cn from "classnames";
import styles from "./index.module.scss";

export interface NavLinkProps {
  to: string;
  color?: "light" | "dark";
  className?: string;
  isAccent?: boolean;
  children?: React.ReactNode;
}

export const NavLinkCustom: FC<NavLinkProps> = ({
  to,
  color,
  className,
  isAccent,
  children,
}) => {
  return (
    <NavLink
      to={to}
      className={cn(
        className,
        {
          [styles.link_accent]: isAccent,
          [styles.link_light]: color === "light",
          [styles.link_dark]: color === "dark",
        }
      )}
    >
      {children}
    </NavLink>
  );
};
