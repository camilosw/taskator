class Task < ActiveRecord::Base
  belongs_to :project
  belongs_to :assigned, class_name: "User"
  belongs_to :creator, class_name: "User"
  attr_accessible :closed, :description, :due_date, :order, :project_id, :assigned_id, :creator_id

  validates :description, presence: true
  validates :project_id, presence: true
  validates :creator_id, presence: true

  def self.active
    tasks = Task.where(closed: false)
  end
end

# == Schema Information
#
# Table name: tasks
#
#  id          :integer          not null, primary key
#  project_id  :integer          not null
#  assigned_id :integer
#  creator_id  :integer          not null
#  description :text             not null
#  due_date    :date
#  order       :integer
#  closed      :boolean          default(FALSE)
#  created_at  :datetime         not null
#  updated_at  :datetime         not null
#

