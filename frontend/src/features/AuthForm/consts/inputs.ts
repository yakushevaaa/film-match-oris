import { InputProps } from "@shared/components/ui/Input";

export const LOGIN_INPUTS: InputProps[] = [
  {
    id: "email",
    type: "email",
    label: "Email",
  },
  {
    id: "password",
    type: "password",
    label: "Password",
  },
];

export const REGISTER_INPUTS: InputProps[] = [
  {
    id: "name",
    type: "text",
    label: "Name",
  },
  {
    id: "email",
    type: "email",
    label: "Email",
  },
];
