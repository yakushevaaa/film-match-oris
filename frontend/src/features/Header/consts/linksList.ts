// types.ts или прямо здесь
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
  "/": [
    {
      label: "Рекомендуемые",
      to: "/recommended",
      color: "light",
      isAccent: true,
    },
    { label: "Профиль", color: "light", to: "/profile" },
  ],
  "/films": [
    { label: "Все фильмы", color: "dark", to: "/films/all" },
    { label: "Избранное", color: "dark", to: "/films/favorites" },
  ],
  "/profile": [
    { label: "Мои данные", color: "dark", to: "/profile/info" },
    { label: "Настройки", color: "dark", to: "/profile/settings" },
  ],
};
