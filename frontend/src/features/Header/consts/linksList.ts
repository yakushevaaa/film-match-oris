export interface NavItem {
  label: string;
  to: string;
  color?: string;
  isAccent?: boolean;
}

type NavConfig = {
  [path: string]: NavItem[];
};

export const NAV_CONFIG: NavConfig = {
  "/profile/friends/": [
    {
      label: "Главная",
      to: "/",
    },
    {
      label: "Мои друзья",
      to: "/profile/friends",
    },
  ],
  "/profile/friends": [
    {
      label: "Главная",
      to: "/",
      isAccent: true,
      color: "light",
    },
    {
      label: "Профиль",
      to: "/profile",
      color: "light",
    },
  ],
  "/profile": [
    {
      label: "Главная",
      to: "/",
      isAccent: true,
      color: "light",
    },
    {
      label: "Мои друзья",
      to: "/profile/friends",
    },
  ],
  "/": [
    {
      label: "Главная",
      to: "/",
      color: "light",
      isAccent: true,
    },
    { label: "Профиль", color: "light", to: "/profile" },
  ],
  "/films": [
    { label: "Главная", color: "dark", to: "/" },
    { label: "Профиль", color: "dark", to: "/profile" },
  ],
};
