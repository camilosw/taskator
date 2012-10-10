class CreateTareas < ActiveRecord::Migration
  def change
    create_table :tareas do |t|
      t.references :proyecto
      t.references :estado
      t.references :user
      t.text :descripcion

      t.timestamps
    end
    add_index :tareas, :proyecto_id
    add_index :tareas, :estado_id
  end
end
