import { FC } from "react";
import { NavLink } from "react-router-dom";
import cn from "classnames";

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
      className={cn(className, {
        link_accent: isAccent,
        link_light: color === "light",
        link_dark: color === "dark",
      })}
    >
      {children}
    </NavLink>
  );
};
