class CreateTasks < ActiveRecord::Migration
  def change
    create_table :tasks do |t|
      t.references :project, null: false
      t.references :assigned
      t.references :creator, null: false
      t.text :description, null: false
      t.date :due_date
      t.integer :order
      t.boolean :closed, default: false

      t.timestamps
    end
    add_index :tasks, :project_id
    add_index :tasks, :assigned_id
    add_index :tasks, :creator_id
  end
end
