class CreateProjects < ActiveRecord::Migration
  def change
    create_table :projects do |t|
      t.references :client
      t.string :name, null: false
      t.text :description
      t.integer :order
      t.boolean :closed, default: false

      t.timestamps
    end
    add_index :projects, :client_id
  end
end
