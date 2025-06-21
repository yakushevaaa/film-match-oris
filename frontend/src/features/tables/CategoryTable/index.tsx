import { useEffect, useState } from "react";
import { MRT_ColumnDef, MRT_Row } from "material-react-table";
import { axiosSettings } from "@shared/api/axiosSettings";
import { AddCategoryModal } from "./AddCategoryModal";
import { DataTable } from "@shared/components/ui/DataTable";

interface Category {
  id: string;
  name: string;
  imageUrl: string;
}

export const CategoryTable = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [editCategory, setEditCategory] = useState<Category | null>(null);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    setIsLoading(true);
    try {
      const res = await axiosSettings.get<Category[]>("/Category/GetCategory");
      setCategories(res.data);
    } catch {
      setCategories([]);
    } finally {
      setIsLoading(false);
    }
  };

  const handleEdit = (row: MRT_Row<Category>) => {
    setEditCategory(row.original);
    setEditModalOpen(true);
  };

  const handleEditSubmit = async (data: any) => {
    try {
      const formData = new FormData();
      formData.append("id", editCategory?.id || "");
      formData.append("name", data.name);
      if (data.image && data.image[0]) {
        formData.append("image", data.image[0]);
      }
      await axiosSettings.put(`/Category/${editCategory?.id}`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      setEditModalOpen(false);
      setEditCategory(null);
      await fetchCategories();
    } catch (error) {
      alert("Ошибка при редактировании категории");
    }
  };

  const handleDelete = async (row: MRT_Row<Category>) => {
    if (!window.confirm("Вы уверены, что хотите удалить категорию?")) return;
    try {
      await axiosSettings.delete(`/Category/${row.original.id}`);
      setCategories((prev) => prev.filter((cat) => cat.id !== row.original.id));
    } catch (error) {
      alert("Ошибка при удалении категории");
    }
  };

  const columns: MRT_ColumnDef<Category>[] = [
    {
      accessorKey: "name",
      header: "Название",
    },
    {
      accessorKey: "imageUrl",
      header: "Картинка",
      Cell: ({ cell }) => (
        <img
          src={cell.getValue<string>()}
          alt="category"
          style={{ width: 60, height: 60, objectFit: "cover" }}
        />
      ),
    },
    {
      accessorKey: "id",
      header: "Id",
    },
  ];

  return (
    <div style={{ padding: "20px" }}>
      <DataTable
        columns={columns}
        data={categories}
        onEdit={handleEdit}
        onDelete={handleDelete}
        isLoading={isLoading}
      />
      {editModalOpen && editCategory && (
        <AddCategoryModal
          isOpen={editModalOpen}
          onClose={() => {
            setEditModalOpen(false);
            setEditCategory(null);
          }}
          onSuccess={fetchCategories}
          editMode={true}
          initialData={{
            ...editCategory,
            imageUrl: editCategory.imageUrl || "",
          }}
          onSubmitCustom={handleEditSubmit}
        />
      )}
    </div>
  );
};
