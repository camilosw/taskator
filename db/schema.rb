# encoding: UTF-8
# This file is auto-generated from the current state of the database. Instead
# of editing this file, please use the migrations feature of Active Record to
# incrementally modify your database, and then regenerate this schema definition.
#
# Note that this schema.rb definition is the authoritative source for your
# database schema. If you need to create the application database on another
# system, you should be using db:schema:load, not running all the migrations
# from scratch. The latter is a flawed and unsustainable approach (the more migrations
# you'll amass, the slower it'll run and the greater likelihood for issues).
#
# It's strongly recommended to check this file into your version control system.

ActiveRecord::Schema.define(:version => 20121023033652) do

  create_table "clients", :force => true do |t|
    t.string   "name",                          :null => false
    t.boolean  "closed",     :default => false
    t.datetime "created_at",                    :null => false
    t.datetime "updated_at",                    :null => false
  end

  create_table "projects", :force => true do |t|
    t.integer  "client_id"
    t.string   "name",                           :null => false
    t.text     "description"
    t.integer  "order"
    t.boolean  "closed",      :default => false
    t.datetime "created_at",                     :null => false
    t.datetime "updated_at",                     :null => false
  end

  add_index "projects", ["client_id"], :name => "index_projects_on_client_id"

  create_table "proyects", :force => true do |t|
    t.integer  "client_id"
    t.string   "name",                           :null => false
    t.text     "description"
    t.integer  "order"
    t.boolean  "closed",      :default => false
    t.datetime "created_at",                     :null => false
    t.datetime "updated_at",                     :null => false
  end

  add_index "proyects", ["client_id"], :name => "index_proyects_on_client_id"

  create_table "tasks", :force => true do |t|
    t.integer  "project_id",                     :null => false
    t.integer  "assigned_id"
    t.integer  "creator_id",                     :null => false
    t.text     "description",                    :null => false
    t.date     "due_date"
    t.integer  "order"
    t.boolean  "closed",      :default => false
    t.datetime "created_at",                     :null => false
    t.datetime "updated_at",                     :null => false
  end

  add_index "tasks", ["assigned_id"], :name => "index_tasks_on_assigned_id"
  add_index "tasks", ["creator_id"], :name => "index_tasks_on_creator_id"
  add_index "tasks", ["project_id"], :name => "index_tasks_on_project_id"

  create_table "users", :force => true do |t|
    t.string   "email",                  :default => "", :null => false
    t.string   "encrypted_password",     :default => "", :null => false
    t.string   "reset_password_token"
    t.datetime "reset_password_sent_at"
    t.datetime "remember_created_at"
    t.integer  "sign_in_count",          :default => 0
    t.datetime "current_sign_in_at"
    t.datetime "last_sign_in_at"
    t.string   "current_sign_in_ip"
    t.string   "last_sign_in_ip"
    t.datetime "created_at",                             :null => false
    t.datetime "updated_at",                             :null => false
  end

  add_index "users", ["email"], :name => "index_users_on_email", :unique => true
  add_index "users", ["reset_password_token"], :name => "index_users_on_reset_password_token", :unique => true

end
